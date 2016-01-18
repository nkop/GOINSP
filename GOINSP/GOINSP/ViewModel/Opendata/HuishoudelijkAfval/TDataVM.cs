using GOINSP.Models;
using GOINSP.Models.Opendata.HuishoudelijkAfval;
using GOINSP.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel.Opendata.HuishoudelijkAfval
{
    public class TDataVM
    {
        private TData tdata;

        public TDataVM()
        {
            tdata = new TData();
        }

        public TDataVM(TData tdata)
        {
            this.tdata = tdata;
        }

        public int ID
        {
            get { return tdata.ID; }
            set { tdata.ID = value; }
        }

        public string RegioS
        {
            get { return tdata.RegioS; }
            set { tdata.RegioS = value.Trim(); }
        }

        public string Perioden
        {
            get { return tdata.Perioden; }
            set { tdata.Perioden = value.Trim(); }
        }

        public string PeriodenFormatted { 
            get { return Perioden.Substring(0, 4); }
        }

        public int? TotaalHuishoudelijkAfval_1
        {
            get { return tdata.TotaalHuishoudelijkAfval_1; }
            set { tdata.TotaalHuishoudelijkAfval_1 = value; }
        }

        public int? HuishoudelijkRestafval_2
        {
            get { return tdata.HuishoudelijkRestafval_2; }
            set { tdata.HuishoudelijkRestafval_2 = value; }
        }

        public int? GrofHuishoudelijkRestafval_3
        {
            get { return tdata.GrofHuishoudelijkRestafval_3; }
            set { tdata.GrofHuishoudelijkRestafval_3 = value; }
        }

        public int? Verbouwingsrestafval_4
        {
            get { return tdata.Verbouwingsrestafval_4; }
            set { tdata.Verbouwingsrestafval_4 = value; }
        }

        public int? GFTAfval_5
        {
            get { return tdata.GFTAfval_5; }
            set { tdata.GFTAfval_5 = value; }
        }

        public int? OudPapierEnKarton_6
        {
            get { return tdata.OudPapierEnKarton_6; }
            set { tdata.OudPapierEnKarton_6 = value; }
        }

        public int? Verpakkingsglas_7
        {
            get { return tdata.Verpakkingsglas_7; }
            set { tdata.Verpakkingsglas_7 = value; }
        }

        public int? Textiel_8
        {
            get { return tdata.Textiel_8; }
            set { tdata.Textiel_8 = value; }
        }

        public int? KleinChemischAfvalKCA_9
        {
            get { return tdata.KleinChemischAfvalKCA_9; }
            set { tdata.KleinChemischAfvalKCA_9 = value; }
        }

        public int? MetalenVerpakkingenBlik_10
        {
            get { return tdata.MetalenVerpakkingenBlik_10; }
            set { tdata.MetalenVerpakkingenBlik_10 = value; }
        }

        public int? Drankenkartons_11
        {
            get { return tdata.Drankenkartons_11; }
            set { tdata.Drankenkartons_11 = value; }
        }

        public int? KunststofVerpakkingen_12
        {
            get { return tdata.KunststofVerpakkingen_12; }
            set { tdata.KunststofVerpakkingen_12 = value; }
        }

        public int? OverigeKunststoffen_13
        {
            get { return tdata.OverigeKunststoffen_13; }
            set { tdata.OverigeKunststoffen_13 = value; }
        }

        public int? Luiers_14
        {
            get { return tdata.Luiers_14; }
            set { tdata.Luiers_14 = value; }
        }

        public int? WitEnBruingoed_15
        {
            get { return tdata.WitEnBruingoed_15; }
            set { tdata.WitEnBruingoed_15 = value; }
        }

        public int? GrofTuinafval_16
        {
            get { return tdata.GrofTuinafval_16; }
            set { tdata.GrofTuinafval_16 = value; }
        }

        public int? BruikbaarHuisraad_17
        {
            get { return tdata.BruikbaarHuisraad_17; }
            set { tdata.BruikbaarHuisraad_17 = value; }
        }

        public int? Vloerbedekking_18
        {
            get { return tdata.Vloerbedekking_18; }
            set { tdata.Vloerbedekking_18 = value; }
        }

        public int? Vlakglas_19
        {
            get { return tdata.Vlakglas_19; }
            set { tdata.Vlakglas_19 = value; }
        }

        public int? Metalen_20
        {
            get { return tdata.Metalen_20; }
            set { tdata.Metalen_20 = value; }
        }

        public int? HoutafvalAEnBHout_21
        {
            get { return tdata.HoutafvalAEnBHout_21; }
            set { tdata.HoutafvalAEnBHout_21 = value; }
        }

        public int? HoutafvalCHout_22
        {
            get { return tdata.HoutafvalCHout_22; }
            set { tdata.HoutafvalCHout_22 = value; }
        }

        public int? SchoonPuin_23
        {
            get { return tdata.SchoonPuin_23; }
            set { tdata.SchoonPuin_23 = value; }
        }

        public int? BitumenhoudendeDakbedekking_24
        {
            get { return tdata.BitumenhoudendeDakbedekking_24; }
            set { tdata.BitumenhoudendeDakbedekking_24 = value; }
        }

        public int? AsbesthoudendAfval_25
        {
            get { return tdata.AsbesthoudendAfval_25; }
            set { tdata.AsbesthoudendAfval_25 = value; }
        }

        public int? Autobanden_26
        {
            get { return tdata.Autobanden_26; }
            set { tdata.Autobanden_26 = value; }
        }

        public int? SchoneGrond_27
        {
            get { return tdata.SchoneGrond_27; }
            set { tdata.SchoneGrond_27 = value; }
        }

        public int? OverigeAfvalstoffen_28
        {
            get { return tdata.OverigeAfvalstoffen_28; }
            set { tdata.OverigeAfvalstoffen_28 = value; }
        }

        public string Gemeentecode_29
        {
            get { return tdata.Gemeentecode_29; }
            set { tdata.Gemeentecode_29 = value.Trim(); }
        }

        public string Provincie_30
        {
            get { return tdata.Provincie_30; }
            set { tdata.Provincie_30 = value.Trim(); }
        }

        public string Stedelijkheid_31
        {
            get { return tdata.Stedelijkheid_31; }
            set { tdata.Stedelijkheid_31 = value.Trim(); }
        }

        public int? InwonersPer1Januari_32
        {
            get { return tdata.InwonersPer1Januari_32; }
            set { tdata.InwonersPer1Januari_32 = value; }
        }

        public void Insert() 
        {
            Config.Context.HuishoudelijkAfvalTData.Add(tdata);
        }
    }
}
