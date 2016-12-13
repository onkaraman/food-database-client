using System;
using System.Xml.Serialization;

namespace FoodDatabase.Core.API.Models.Item
{
    /// <summary>
    /// Contains the data of a food item.
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Will return true if this food item is measured in grams.
        /// </summary>
        public bool IsGramMeasurement
        {
            get
            {
                return amount_measuring_system.ToLower().Contains("gram");    
            }
        }

        /// <summary>
        /// Will return true if this food item is measured in millilitre.
        /// </summary>
        public bool IsMLMeasurement
        {
            get
            {
                return amount_measuring_system.ToLower().Contains("ml");
            }
        }

        public string diary_serving_amount { get; set; }
        public string diary_serving_name { get; set; }
        public string amount { get; set; }
        public string amount_measuring_system { get; set; }
        public string aggregate_state { get; set; }
        public string kj { get; set; }

        private int _kcal;
        public string kcal { get { return _kcal.ToString(); } set { _kcal = ((int) double.Parse(value)); } }

        private int _fat;
        public string fat_gram { get { return _fat.ToString(); } set { _fat = ((int)double.Parse(value)); } }

        private int _fatSat;
        public string fat_sat_gram { get { return _fatSat.ToString(); } set { _fatSat = ((int)double.Parse(value)); } }

        private int _kh;
        public string kh_gram { get { return _kh.ToString(); } set { _kh = ((int)double.Parse(value)); } }

        private int _sugar;
        public string sugar_gram { get { return _sugar.ToString(); } set { _sugar = ((int)double.Parse(value)); } }

        private int _protein;
        public string protein_gram { get { return _protein.ToString(); } set { _protein = ((int)double.Parse(value)); } }

        public string cholesterol_mg { get; set; }
        public string df_gram { get; set; }
        public string water_gram { get; set; }
        public string alcohol_gram { get; set; }
        public string v_a_mg { get; set; }
        public string v_b1_mg { get; set; }
        public string v_b2_mg { get; set; }
        public string v_b6_mg { get; set; }
        public string v_b12_mg { get; set; }
        public string v_c_mg { get; set; }
        public string v_d_mg { get; set; }
        public string v_e_mg { get; set; }
        public string m_eisen_mg { get; set; }
        public string m_calcium_mg { get; set; }
        public string m_magnesium_mg { get; set; }
        public string m_salt_g { get; set; }
        public string m_zink_mg { get; set; }
        public string m_kupfer_mg { get; set; }
        public string m_schwefel_mg { get; set; }
        public string m_mangan_mg { get; set; }
        public string m_chlor_mg { get; set; }
        public string m_fluor_mg { get; set; }
        public string m_kalium_mg { get; set; }
        public string m_phosphor_mg { get; set; }
        public string m_iod_mg { get; set; }
    }
}
