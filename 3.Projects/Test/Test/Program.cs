using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DAS;

namespace Test
{
    class Program
    {
        static string GetMenuLine(string key, string functionName)
        {
            return string.Format("input {0} for {1}", key, functionName);
        }
        static Dictionary<string, string> KeyFunctions = new Dictionary<string, string>()
        {
            { "q","Exiting"},
            { "i","Insert"},
            { "mi","MultiInsert"},
            { "fe","FindEach"},
            { "fl","FindList"},
            { "fo","FindOne"},
            { "t","Tester"}
        };
        static void DisplayMenu()
        {
            List<string> menu = new List<string>()
            {
                "-------一------",
                "this is a menu",
                "-------一------"
            };
            foreach (var item in menu)
            {
                Console.WriteLine(item);
            }
            foreach (var item in KeyFunctions)
            {
                Console.WriteLine(GetMenuLine(item.Key, item.Value));
            }
        }
        //static Session _session = null;
        //static Session Session
        //{
        //    get
        //    {
        //        if (_sessi)
        //        {
        //            _session = new Session();
        //            _session.Connect();
        //        }
        //        return _session;
        //    }
        //}

        public abstract class IA
        {
            public static string Name { set; get; }
        }
        public class A1: IA
        {
            static A1()
            {
                Name = nameof(A1);
            }
        }
        public class A2 : IA
        {
            static A2()
            {
                Name = nameof(A2);
            }
        }


        static void Main(string[] args)
        {
            //var A1 = new A1();
            //string a1Name = A1.Name;
            //var A2 = new A2();
            //string a2Name = A2.Name;



            //int hitCount = 0;
            //foreach (var recent10Result in "10101011")
            //{
            //    if (recent10Result == '1')
            //    {
            //        hitCount++;
            //    }
            //}

            //bool isOk = true;
            //NullableParameterTest(isOk);

            //Session Session = new Session();
            //Session.Connect();

            DisplayMenu();
            string input = "";
            while ((input = Console.ReadLine()) != "q")
            {
                switch (input)
                {
                    //case "i":
                    //    Session.InsertTempObject();
                    //    break;
                    //case "mi":
                    //    SessionHelper helper = new SessionHelper(Session);
                    //    DateTime start = DateTime.Now;
                    //    for (int i = 0; i < 1000; i++)
                    //    {
                    //        Session.MultiInsert(helper.GetNewId());
                    //    }
                    //    DateTime end = DateTime.Now;
                    //    TimeSpan duration = start - end;
                    //    break;
                    //case "fe":
                    //    Session.FindEach();
                    //    break;
                    //case "fl":
                    //    Session.Find();
                    //    break;
                    case "fo":
                        //Session.Find();
                        break;
                    case "t":
                        new Tester().test();
                        break;
                    default:
                        break;
                }
                DisplayMenu();
            }
        }
        public static void NullableParameterTest(bool? isThatOk)
        {

        }
    }
}
