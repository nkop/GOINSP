﻿using GalaSoft.MvvmLight;
using GOINSP.Models;
using GOINSP.Models.Opendata.PostCodeData;
using GOINSP.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel.Opendata.PostCodeDatas
{
    public class PostCodeDataVM : ViewModelBase
    {
        private PostCodeData postCodeData;

        public PostCodeDataVM()
        {
            postCodeData = new PostCodeData();
        }

        public Guid PostCodeID
        {
            get { return postCodeData.PostCodeID; }
            set { postCodeData.PostCodeID = value; }
        }

        public string postcode
        {
            get { return postCodeData.postcode; }
            set { postCodeData.postcode = value; }
        }

        public int postcode_id
        {
            get { return postCodeData.postcode_id; }
            set { postCodeData.postcode_id = value; }
        }

        public int pnum
        {
            get { return postCodeData.pnum; }
            set { postCodeData.pnum = value; }
        }

        public string pchar
        {
            get { return postCodeData.pchar; }
            set { postCodeData.pchar = value; }
        }

        public int minnumber
        {
            get { return postCodeData.minnumber; }
            set { postCodeData.minnumber = value; }
        }

        public int maxnumber
        {
            get { return postCodeData.maxnumber; }
            set { postCodeData.maxnumber = value; }
        }

        public string numbertype
        {
            get { return postCodeData.numbertype; }
            set { postCodeData.numbertype = value; }
        }

        public string street
        {
            get { return postCodeData.street; }
            set { postCodeData.street = value; }
        }

        public string city
        {
            get { return postCodeData.city; }
            set { postCodeData.city = value; }
        }

        public int city_id
        {
            get { return postCodeData.city_id; }
            set { postCodeData.city_id = value; }
        }

        public string municipality
        {
            get { return postCodeData.municipality; }
            set 
            { 
                postCodeData.municipality = value; 
                RaisePropertyChanged("municipality"); 
            }
        }

        public string municipality_id
        {
            get { return postCodeData.municipality_id; }
            set { postCodeData.municipality_id = value; }
        }

        public string province
        {
            get { return postCodeData.province; }
            set { postCodeData.province = value; }
        }

        public string province_code
        {
            get { return postCodeData.province_code; }
            set { postCodeData.province_code = value; }
        }

        public decimal lat
        {
            get { return postCodeData.lat; }
            set { postCodeData.lat = value; }
        }

        public decimal lon
        {
            get { return postCodeData.lon; }
            set { postCodeData.lon = value; }
        }

        public decimal rd_x
        {
            get { return postCodeData.rd_x; }
            set { postCodeData.rd_x = value; }
        }

        public decimal rd_y
        {
            get { return postCodeData.rd_y; }
            set { postCodeData.rd_y = value; }
        }

        public string location_detail
        {
            get { return postCodeData.location_detail; }
            set { postCodeData.location_detail = value; }
        }
        public int? street_number
        {
            get { return postCodeData.street_number; }
            set { postCodeData.street_number = value; }
        }

        public DateTime changed_date
        {
            get { return postCodeData.changed_date; }
            set { postCodeData.changed_date = value; }
        }

        public PostCodeData getObject()
        {
            return postCodeData;
        }

        public void Insert()
        {
            Config.Context.PostCodeData.Add(postCodeData);
            Config.Context.SaveChanges();
        }
    }
}
