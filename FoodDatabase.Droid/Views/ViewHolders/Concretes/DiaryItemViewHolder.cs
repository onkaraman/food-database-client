using System.Threading;
using Android.App;
using Android.Widget;
using FoodDatabase.Core.API.Models.Diary;
using FoodDatabase.Core.Managers;
using FoodDatabase.Droid.Views.Adapters;
using FoodDatabase.Droid.Views.Widgets;
using UniversalImageLoader.Core;

namespace FoodDatabase.Droid.Views.ViewHolders.Concretes
{
    public class DiaryItemViewHolder : GeneralViewHolder
    {
        public ImageView Thumbnail { get; set; }
        public MainTextView Name { get; set; }
        public MainTextView Producer { get; set; }
        public MainTextView Kcal { get; set; }
        public MainTextView Proteins { get; set; }
        public MainTextView Carbohydrates { get; set; }
        public MainTextView Sugar { get; set; }
        public MainTextView Fat { get; set; }

        /// <summary>
        /// Will apply the passed meal data to the UI elements.
        /// </summary>
        public void ApplyData(DiaryElement diaryElement, Activity a)
        {
            ThreadPool.QueueUserWorkItem(o => loadThumbnail(diaryElement, a));

            Name.Text = shortenName(diaryElement.DiaryShortItem.Description.name, 25);
            Producer.Text = string.Format("{0}{1} ({2})", diaryElement.DiaryShortItem.Data.diary_serving_amount, 
                                          diaryElement.DiaryShortItem.Data.GetMeasureUnit(),
                                          diaryElement.DateInTime.ToString("HH:mm"));
            Kcal.Text = string.Format("{0}", diaryElement.DiaryShortItem.Data.kcal_diary);
            Proteins.Text = string.Format("{0}g", diaryElement.DiaryShortItem.Data.protein_gram);
            Carbohydrates.Text = string.Format("{0}g", diaryElement.DiaryShortItem.Data.kh_gram);
            Sugar.Text = string.Format("{0}g", diaryElement.DiaryShortItem.Data.sugar_gram);
            Fat.Text = string.Format("{0}g", diaryElement.DiaryShortItem.Data.fat_diary);
        }

        /// <summary>
        /// Will return the passed item name in substringed if required.
        /// </summary>
        private string shortenName(string name, int cutLength)
        {
            if (name.Length > cutLength)
                return name.Substring(0, cutLength).Trim();
            return name.Trim();
        }

        /// <summary>
        /// Will load the thumbnail into the ImageView UI.
        /// </summary>
        private void loadThumbnail(DiaryElement item, Activity a)
        {
            try
            {
                a.RunOnUiThread(() =>
                {
                    string url = PersistenceManager.Static.GetFirst(string.Format("{0}-img", item.DiaryShortItem.id)).Value;
                    ImageLoader.Instance.DisplayImage(url, Thumbnail);
                });
            }
            catch
            {
                //TODO: Reporter here.
            }
        }
    }
}
