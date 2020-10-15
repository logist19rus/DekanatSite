using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceReference2;

namespace TTZaebal.Models
{
    public class perfomanceClass
    {
        List<item> items = new List<item>();
        public perfomanceClass(List<GradesStudentsDto> grades)
        {
            foreach (var x in grades)
            {
                items.Add(new item(x));
            }
        }
        public string getPerfTable()
        {
            string newStr = "";

            newStr += "<div class=\"perfomance_table\"> <table><thead><tr>";
            newStr += "<th>Предмет</th><th>Тип контроля</th><th>Оценка</th>";
            newStr += "</tr></thead><tbody>";
            foreach(var x in items)
            {
                newStr += x.getStr();
            }
            newStr += "</tbody></table></div>";

            return newStr;
        }
        public class item
        {
            string name;
            string typeZ;
            string value;
            public item(GradesStudentsDto grades)
            {
                name = grades.Subject;
                typeZ = grades.ControlType;
                value = grades.Score;
            }
            public string getStr()
            {
                string newStr = "";
                newStr += "<tr><td>";
                newStr += name;
                newStr += "</td><td>";
                newStr += typeZ;
                newStr += "</td><td>";
                newStr += value;
                newStr += "</td></tr>";
                return newStr;
            }
        }
    }

    public class perfomanceSelector
    {
        private int startYear = 2019;
        List<string> Years = new List<string>();
        List<string> Sessions = new List<string>() { "Летняя", "Зимняя" };
        public perfomanceSelector()
        {
            int actualYear = DateTime.Now.Year;

            for(int i = startYear; i <= actualYear; i++)
            {
                string nowStr = "";
                nowStr += i.ToString();
                nowStr += "-";
                nowStr += (i + 1).ToString();
                Years.Add(nowStr);
            }
        }

        public string getYearSel()
        {
            // <option value="2019-2020">2019-2020</option>
            string result = "";

            foreach(var x in Years)
            {
                result += "<option value=\"";
                result += x + "\">" + x;
                result += "</option>";
            }

            return result;
        }
    }
}
