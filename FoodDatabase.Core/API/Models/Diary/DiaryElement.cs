using System;
using System.Xml.Serialization;

namespace FoodDatabase.Core.API.Models.Diary
{
    /// <summary>
    /// A diary element of a user holds actual and meta information.
    /// </summary>
    public class DiaryElement
    {
        public string diary_uid { get; set; }

        public DateTime DateInTimeStamp
        {
            get
            {
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                return dtDateTime.AddSeconds(int.Parse(diary_date)).ToLocalTime();
            }
        }
        public string diary_date { get; set; }
        public string diary_type { get; set; }

        [XmlElement("diaryshortitem")]
        public DiaryShortItem DiaryShortItem { get; set; }
    }
}
