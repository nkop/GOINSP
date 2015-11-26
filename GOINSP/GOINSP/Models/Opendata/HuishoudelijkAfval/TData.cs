using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models.Opendata.HuishoudelijkAfval
{
    public class TData
    {
        public TData()
        {

        }

        [Key]
        public int ID { get; set; }
        public string RegioS { get; set; }
        public string Perioden { get; set; }
        public int? TotaalHuishoudelijkAfval_1 { get; set; }
        public int? HuishoudelijkRestafval_2 { get; set; }
        public int? GrofHuishoudelijkRestafval_3 { get; set; }
        public int? Verbouwingsrestafval_4 { get; set; }
        public int? GFTAfval_5 { get; set; }
        public int? OudPapierEnKarton_6 { get; set; }
        public int? Verpakkingsglas_7 { get; set; }
        public int? Textiel_8 { get; set; }
        public int? KleinChemischAfvalKCA_9 { get; set; }
        public int? MetalenVerpakkingenBlik_10 { get; set; }
        public int? Drankenkartons_11 { get; set; }
        public int? KunststofVerpakkingen_12 { get; set; }
        public int? OverigeKunststoffen_13 { get; set; }
        public int? Luiers_14 { get; set; }
        public int? WitEnBruingoed_15 { get; set; }
        public int? GrofTuinafval_16 { get; set; }
        public int? BruikbaarHuisraad_17 { get; set; }
        public int? Vloerbedekking_18 { get; set; }
        public int? Vlakglas_19 { get; set; }
        public int? Metalen_20 { get; set; }
        public int? HoutafvalAEnBHout_21 { get; set; }
        public int? HoutafvalCHout_22 { get; set; }
        public int? SchoonPuin_23 { get; set; }
        public int? BitumenhoudendeDakbedekking_24 { get; set; }
        public int? AsbesthoudendAfval_25 { get; set; }
        public int? Autobanden_26 { get; set; }
        public int? SchoneGrond_27 { get; set; }
        public int? OverigeAfvalstoffen_28 { get; set; }
        public string Gemeentecode_29 { get; set; }
        public string Provincie_30 { get; set; }
        public string Stedelijkheid_31 { get; set; }
        public int? InwonersPer1Januari_32 { get; set; }
    }
}
