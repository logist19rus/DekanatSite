using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceReference1;

namespace TTZaebal.Models
{
    public class Week
    {
        List<TimetableDTO> TTFromDB;
        public DayTT[] days = new DayTT[7];
        public static string dateString = "";

        public Week(List<TimetableDTO> _TTFromDB)
        {
            TTFromDB = _TTFromDB;
        }
        public void WeekCreater(string dayofweek)
        {
            var usedDate = DateTime.Today;
            if (dayofweek!="000")
            {
                usedDate = DateTime.Parse(dayofweek);
            }
            
            var actualMonday = usedDate.AddDays(-(int)usedDate.DayOfWeek + 1);
            var actualSaturday = actualMonday.AddDays(6);
            getActualDiap(actualMonday, actualSaturday);

            var actualWeek = TTFromDB.Where(x => x.DateSubject >= actualMonday && x.DateSubject <= actualSaturday);

            for(int i = 0; i < 7; i++)
            {
                days[i] = new DayTT(actualWeek.Where(x => x.DateSubject == actualMonday.AddDays(i)).ToList(), i);
            }
        }
        public void getActualDiap(DateTime start, DateTime end)
        {
            dateString = "";
            dateString += start.Day + "." + start.Month + "." + start.Year + "–";
            dateString += end.Day + "." + end.Month + "." + end.Year;
        }

        public DayTT[] getWeek()
        {
            return days;
        }
        public class DayTT
        {
            public Lecture[] lectures = new Lecture[6];

            public DayTT(List<TimetableDTO> nowUsedDay, int id)
            {
                foreach(var x in nowUsedDay)
                {
                    if(lectures[(int)x.NumberLecture - 1] == null)
                    {
                        lectures[(int)x.NumberLecture - 1] = new Lecture(x);
                    }
                    else
                    {
                        lectures[(int)x.NumberLecture - 1].add(x);
                    }
                }
            }

            public class Lecture
            {
                public bool isDoule = false;
                string[] Lectures = new string[2] { "", "" };
                string PName;
                string Teacher;
                string lecType;
                string kabNum;
                string zoomLink;

                public Lecture(TimetableDTO UsedDay)
                {
                    fillInfo(UsedDay);
                    int sg = (int)UsedDay.SubGroupNumber;
                    if (sg > 0)
                    {
                        Lectures[sg - 1] = createLec();
                        if (!isDoule) isDoule = true;
                    }
                }
                void fillInfo(TimetableDTO UsedDay)
                {
                    this.PName = UsedDay.Subject;
                    this.Teacher = UsedDay.NameTeacher;
                    this.lecType = UsedDay.TypeLecture;
                    this.kabNum = UsedDay.NumberCabinet;
                    this.zoomLink = UsedDay.LinkLecture;
                }
                public void add(TimetableDTO UsedDay)
                {
                    fillInfo(UsedDay);
                    int sg = (int)UsedDay.SubGroupNumber;
                    if (sg > 0)
                    {
                        Lectures[sg - 1] = createLec();
                        if(!isDoule) isDoule = true;
                    }
                }
                public string getStr()
                {
                    string newStr = "";
                    if (isDoule)
                    {
                        newStr += "<div class=\"cell_sub\">" + Lectures[0] + "</div>";
                        newStr += "<div class=\"cell_sub\">" + Lectures[1] + "</div>";
                    }
                    else
                    {

                        newStr = createLec();
                    }
                    return newStr;
                }
                string createLec()
                {
                    string newStr = "";
                    if (this.zoomLink!=null && this.zoomLink != "")
                    {
                        newStr += "<a href=\""+ this.zoomLink +"\">";
                    }
                    //newStr += this.PName;
                    newStr += createRef(this.PName);
                    newStr += this.lecType;
                    newStr += "<br>";
                    newStr += this.Teacher;
                    if(this.kabNum!=null && this.kabNum != "")
                    {
                        newStr += "<br>";
                        newStr += this.kabNum;
                    }
                    if (this.zoomLink != null && this.zoomLink != "")
                    {
                        newStr += "</a>";
                    }
                    return newStr;
                }
                string createRef(string old)
                {
                    string newstr = "";
                    while (old.Length > 7)
                    {
                        newstr += old.Substring(0, 7) + "&shy;";
                        old = old.Substring(7);
                    }
                    newstr += old;
                    return newstr;
                }
            }
        }
    }
}
