using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models.opendata
{
    public class HuishoudelijkAfval
    {
        public HuishoudelijkAfval() 
        {

        }

        [Key]
        public int ID { get; set; }
        public string RegioS { get; set; }
        public string Perioden { get; set; }
        public string TotaalHuishoudelijkAfval_1 { get; set; }
        public string HuishoudelijkRestafval_2 { get; set; }
        public string GrofHuishoudelijkRestafval_3 { get; set; }
        public string Verbouwingsrestafval_4 { get; set; }
        public string GFTAfval_5 { get; set; }
        public string OudPapierEnKarton_6 { get; set; }
        public string Verpakkingsglas_7 { get; set; }
        public string Textiel_8 { get; set; }
        public string KleinChemischAfvalKCA_9 { get; set; }
        public string MetalenVerpakkingenBlik_10 { get; set; }
        public string Drankenkartons_11 { get; set; }
        public string KunststofVerpakkingen_12 { get; set; }
        public string OverigeKunststoffen_13 { get; set; }
        public string Luiers_14 { get; set; }
        public string WitEnBruingoed_15 { get; set; }
        public string GrofTuinafval_16 { get; set; }
        public string BruikbaarHuisraad_17 { get; set; }
        public string Vloerbedekking_18 { get; set; }
        public string Vlakglas_19 { get; set; }
        public string Metalen_20 { get; set; }
        public string HoutafvalAEnBHout_21 { get; set; }
        public string HoutafvalCHout_22 { get; set; }
        public string SchoonPuin_23 { get; set; }
        public string BitumenhoudendeDakbedekking_24 { get; set; }
        public string AsbesthoudendAfval_25 { get; set; }
        public string Autobanden_26 { get; set; }
        public string SchoneGrond_27 { get; set; }
        public string OverigeAfvalstoffen_28 { get; set; }
        public string Gemeentecode_29 { get; set; }
        public string Provincie_30 { get; set; }
        public string Stedelijkheid_31 { get; set; }
        public string InwonersPer1Januari_32 { get; set; }
    }
}
