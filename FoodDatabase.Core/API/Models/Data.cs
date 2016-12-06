﻿using System;
namespace FoodDatabase.Core.API.Models
{
    /// <summary>
    /// Contains the data of a food item.
    /// </summary>
    public class Data
    {
        public string amount { get; set; }
        // Per hand
        public string amount_measuring_system { get; set; }
        // Per hand
        public string aggregate_state { get; set; }
        public string kj { get; set; }
        public string kcal { get; set; }
        public string fat_gram { get; set; }
        public string fat_sat_gram { get; set; }
        public string kh_gram { get; set; }
        public string sugar_gram { get; set; }
        public string cholesterol_mg { get; set; }
        public string protein_gram { get; set; }
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
