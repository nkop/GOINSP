using GOINSP.Models;
using GOINSP.Models.opendata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel.Opendata
{
    public class HuishoudelijkAfvalVM
    {
        private HuishoudelijkAfval huishoudelijkAfval;
        private Context context;

        public HuishoudelijkAfvalVM()
        {
            huishoudelijkAfval = new HuishoudelijkAfval();
            context = new Context();
        }

        public int ID
        {
            get { return huishoudelijkAfval.ID; }
            set { huishoudelijkAfval.ID = value; }
        }

        public string RegioS
        {
            get { return huishoudelijkAfval.RegioS; }
            set { huishoudelijkAfval.RegioS = value.Trim(); }
        }

        public string Perioden
        {
            get { return huishoudelijkAfval.Perioden; }
            set { huishoudelijkAfval.Perioden = value.Trim(); }
        }

        public string TotaalHuishoudelijkAfval_1
        {
            get { return huishoudelijkAfval.TotaalHuishoudelijkAfval_1; }
            set { huishoudelijkAfval.TotaalHuishoudelijkAfval_1 = value.Trim(); }
        }

        public string HuishoudelijkRestafval_2
        {
            get { return huishoudelijkAfval.HuishoudelijkRestafval_2; }
            set { huishoudelijkAfval.HuishoudelijkRestafval_2 = value.Trim(); }
        }

        public string GrofHuishoudelijkRestafval_3
        {
            get { return huishoudelijkAfval.GrofHuishoudelijkRestafval_3; }
            set { huishoudelijkAfval.GrofHuishoudelijkRestafval_3 = value.Trim(); }
        }

        public string Verbouwingsrestafval_4
        {
            get { return huishoudelijkAfval.Verbouwingsrestafval_4; }
            set { huishoudelijkAfval.Verbouwingsrestafval_4 = value.Trim(); }
        }

        public string GFTAfval_5
        {
            get { return huishoudelijkAfval.GFTAfval_5; }
            set { huishoudelijkAfval.GFTAfval_5 = value.Trim(); }
        }

        public string OudPapierEnKarton_6
        {
            get { return huishoudelijkAfval.OudPapierEnKarton_6; }
            set { huishoudelijkAfval.OudPapierEnKarton_6 = value.Trim(); }
        }

        public string Verpakkingsglas_7
        {
            get { return huishoudelijkAfval.Verpakkingsglas_7; }
            set { huishoudelijkAfval.Verpakkingsglas_7 = value.Trim(); }
        }

        public string Textiel_8
        {
            get { return huishoudelijkAfval.Textiel_8; }
            set { huishoudelijkAfval.Textiel_8 = value.Trim(); }
        }

        public string KleinChemischAfvalKCA_9
        {
            get { return huishoudelijkAfval.KleinChemischAfvalKCA_9; }
            set { huishoudelijkAfval.KleinChemischAfvalKCA_9 = value.Trim(); }
        }

        public string MetalenVerpakkingenBlik_10
        {
            get { return huishoudelijkAfval.MetalenVerpakkingenBlik_10; }
            set { huishoudelijkAfval.MetalenVerpakkingenBlik_10 = value.Trim(); }
        }

        public string Drankenkartons_11
        {
            get { return huishoudelijkAfval.Drankenkartons_11; }
            set { huishoudelijkAfval.Drankenkartons_11 = value.Trim(); }
        }

        public string KunststofVerpakkingen_12
        {
            get { return huishoudelijkAfval.KunststofVerpakkingen_12; }
            set { huishoudelijkAfval.KunststofVerpakkingen_12 = value.Trim(); }
        }

        public string OverigeKunststoffen_13
        {
            get { return huishoudelijkAfval.OverigeKunststoffen_13; }
            set { huishoudelijkAfval.OverigeKunststoffen_13 = value.Trim(); }
        }

        public string Luiers_14
        {
            get { return huishoudelijkAfval.Luiers_14; }
            set { huishoudelijkAfval.Luiers_14 = value.Trim(); }
        }

        public string WitEnBruingoed_15
        {
            get { return huishoudelijkAfval.WitEnBruingoed_15; }
            set { huishoudelijkAfval.WitEnBruingoed_15 = value.Trim(); }
        }

        public string GrofTuinafval_16
        {
            get { return huishoudelijkAfval.GrofTuinafval_16; }
            set { huishoudelijkAfval.GrofTuinafval_16 = value.Trim(); }
        }

        public string BruikbaarHuisraad_17
        {
            get { return huishoudelijkAfval.BruikbaarHuisraad_17; }
            set { huishoudelijkAfval.BruikbaarHuisraad_17 = value.Trim(); }
        }

        public string Vloerbedekking_18
        {
            get { return huishoudelijkAfval.Vloerbedekking_18; }
            set { huishoudelijkAfval.Vloerbedekking_18 = value.Trim(); }
        }

        public string Vlakglas_19
        {
            get { return huishoudelijkAfval.Vlakglas_19; }
            set { huishoudelijkAfval.Vlakglas_19 = value.Trim(); }
        }

        public string Metalen_20
        {
            get { return huishoudelijkAfval.Metalen_20; }
            set { huishoudelijkAfval.Metalen_20 = value.Trim(); }
        }

        public string HoutafvalAEnBHout_21
        {
            get { return huishoudelijkAfval.HoutafvalAEnBHout_21; }
            set { huishoudelijkAfval.HoutafvalAEnBHout_21 = value.Trim(); }
        }

        public string HoutafvalCHout_22
        {
            get { return huishoudelijkAfval.HoutafvalCHout_22; }
            set { huishoudelijkAfval.HoutafvalCHout_22 = value.Trim(); }
        }

        public string SchoonPuin_23
        {
            get { return huishoudelijkAfval.SchoonPuin_23; }
            set { huishoudelijkAfval.SchoonPuin_23 = value.Trim(); }
        }

        public string BitumenhoudendeDakbedekking_24
        {
            get { return huishoudelijkAfval.BitumenhoudendeDakbedekking_24; }
            set { huishoudelijkAfval.BitumenhoudendeDakbedekking_24 = value.Trim(); }
        }

        public string AsbesthoudendAfval_25
        {
            get { return huishoudelijkAfval.AsbesthoudendAfval_25; }
            set { huishoudelijkAfval.AsbesthoudendAfval_25 = value.Trim(); }
        }

        public string Autobanden_26
        {
            get { return huishoudelijkAfval.Autobanden_26; }
            set { huishoudelijkAfval.Autobanden_26 = value.Trim(); }
        }

        public string SchoneGrond_27
        {
            get { return huishoudelijkAfval.SchoneGrond_27; }
            set { huishoudelijkAfval.SchoneGrond_27 = value.Trim(); }
        }

        public string OverigeAfvalstoffen_28
        {
            get { return huishoudelijkAfval.OverigeAfvalstoffen_28; }
            set { huishoudelijkAfval.OverigeAfvalstoffen_28 = value.Trim(); }
        }

        public string Gemeentecode_29
        {
            get { return huishoudelijkAfval.Gemeentecode_29; }
            set { huishoudelijkAfval.Gemeentecode_29 = value.Trim(); }
        }

        public string Provincie_30
        {
            get { return huishoudelijkAfval.Provincie_30; }
            set { huishoudelijkAfval.Provincie_30 = value.Trim(); }
        }

        public string Stedelijkheid_31
        {
            get { return huishoudelijkAfval.Stedelijkheid_31; }
            set { huishoudelijkAfval.Stedelijkheid_31 = value.Trim(); }
        }

        public string InwonersPer1Januari_32
        {
            get { return huishoudelijkAfval.InwonersPer1Januari_32; }
            set { huishoudelijkAfval.InwonersPer1Januari_32 = value.Trim(); }
        }

        public void Insert()
        {
            context.HuishoudelijkAfval.Add(huishoudelijkAfval);
            context.SaveChanges();
        }
    }
}
