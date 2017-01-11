using System.Collections.Generic;
using FoodDatabase.Core.Helpers;

namespace FoodDatabase.Core.API.Models.Items
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
        public string kcal { get { return _kcal.ToString(); } set { _kcal = ((int)double.Parse(value)); } }
        public int kcal_diary { get { return (int)(_kcal * (double.Parse(diary_serving_amount) / 100)); } }

        private int _fat;
        public string fat_gram { get { return _fat.ToString(); } set { _fat = ((int)double.Parse(value)); } }
        public int fat_diary { get { return (int) (_fat * (double.Parse(diary_serving_amount) / 100)); } }

        private int _fatSat;
        public string fat_sat_gram { get { return _fatSat.ToString(); } set { _fatSat = ((int)double.Parse(value)); } }
        public int fat_sat_diary { get { return (int)(_fatSat * (double.Parse(diary_serving_amount) / 100)); } }

        private int _kh;
        public string kh_gram { get { return _kh.ToString(); } set { _kh = ((int)double.Parse(value)); } }
        public int kh_diary { get { return (int)(_kh * (double.Parse(diary_serving_amount) / 100)); } }

        private int _sugar;
        public string sugar_gram { get { return _sugar.ToString(); } set { _sugar = ((int)double.Parse(value)); } }
        public int sugar_diary { get { return (int)(_sugar * (double.Parse(diary_serving_amount) / 100)); } }

        private int _protein;
        public string protein_gram { get { return _protein.ToString(); } set { _protein = ((int)double.Parse(value)); } }
        public int protein_diary { get { return (int)(_protein * (double.Parse(diary_serving_amount) / 100)); } }

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

        /// <summary>
        /// Will return every held data in prepared list form. 
        /// </summary>
        public List<ItemDataHolder> SubdataAsList()
        {
            List<ItemDataHolder> list = new List<ItemDataHolder>();
            list.Add(new ItemDataHolder
            {
                Name = "Kcal",
                Value = kcal,
                Unit = ""
            });
            list.Add(new ItemDataHolder
            {
                Name = "Proteins",
                Value = protein_gram,
                Unit = "g"
            });
            list.Add(new ItemDataHolder
            {
                Name = "Fat",
                Value = fat_gram,
                Unit = "g"
            });
            list.Add(new ItemDataHolder
            {
                Name = "Fat (saturated)",
                Value = fat_sat_gram,
                Unit = "g"
            });
            list.Add(new ItemDataHolder
            {
                Name = "Carbohydrates",
                Value = kh_gram,
                Unit = "g"
            });
            list.Add(new ItemDataHolder
            {
                Name = "Sugar",
                Value = sugar_gram,
                Unit = "g"
            });
            list.Add(new ItemDataHolder
            {
                Name = "Cholesterol",
                Value = cholesterol_mg,
                Unit = "g"
            });
            list.Add(new ItemDataHolder
            {
                Name = "Diatery Fiber",
                Value = df_gram,
                Unit = "g"
            });
            list.Add(new ItemDataHolder
            {
                Name = "Water",
                Value = water_gram,
                Unit = "ml"
            });
            list.Add(new ItemDataHolder
            {
                Name = "Alcohol",
                Value = alcohol_gram,
                Unit = "ml"
            });
            list.Add(new ItemDataHolder
            {
                Name = "Vitamine A",
                Value = v_a_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Vitamine B1",
                Value = v_b1_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Vitamine B2",
                Value = v_b2_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Vitamine B6",
                Value = v_b6_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Vitamine B12",
                Value = v_b12_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Vitamine C",
                Value = v_c_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Vitamine D",
                Value = v_d_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Vitamine E",
                Value = v_e_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Iron",
                Value = m_eisen_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Calcium",
                Value = m_calcium_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Magnesium",
                Value = m_magnesium_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Salt",
                Value = m_salt_g,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Zinc",
                Value = m_zink_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Copper",
                Value = m_kupfer_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Sulfur",
                Value = m_schwefel_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Manganese",
                Value = m_mangan_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Chlorine",
                Value = m_chlor_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Fluorine",
                Value = m_fluor_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Potassium",
                Value = m_kalium_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Phosphor",
                Value = m_phosphor_mg,
                Unit = "mg",
            });
            list.Add(new ItemDataHolder
            {
                Name = "Iodine",
                Value = m_iod_mg,
                Unit = "mg",
            });

            return cleanUp(list);
        }

        /// <summary>
        /// Will give IDs to the list elements and remove those with
        /// no values.
        /// </summary>
        private List<ItemDataHolder> cleanUp(List<ItemDataHolder> list)
        {
            List<ItemDataHolder> ret = new List<ItemDataHolder>();

            for (int i = 0; i < list.Count; i += 1)
            {
                if (list[i].Value != null && list[i].Value.Length > 0)
                {
                    list[i].id = i;
                    if (list[i].Value.Length > 4) list[i].Value = list[i].Value.Substring(0, 4);
                    ret.Add(list[i]);
                }
            }
            return ret;
        }
    
        /// <summary>
        /// Will return the mesaurement unit of this food item's data.
        /// </summary>
        public string GetMeasureUnit()
        {
            if (IsMLMeasurement) return "ml";
            if (IsGramMeasurement) return "g";
            return "?";
        }
    }
}
