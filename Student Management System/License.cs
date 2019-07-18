using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Linq.Mapping;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System
{
    [Table(Name = "License")]
    class License
    {
        [Display(Name = "ID")]
        [Column(Name = "ID", IsDbGenerated = true, IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int ID { get; set; }

        [Display(Name = "SoftwareID")]
        [Column(Name = "SoftwareID", DbType = "NVARCHAR")]
        public string SoftwareID { get; set; }

        [Display(Name = "OwnerName")]
        [Column(Name = "OwnerName", DbType = "NVARCHAR")]
        public string OwnerName { get; set; }

        [Display(Name = "SchoolName")]
        [Column(Name = "SchoolName", DbType = "NVARCHAR")]
        public string SchoolName { get; set; }

        [Display(Name = "SchoolAddress")]
        [Column(Name = "SchoolAddress", DbType = "NVARCHAR")]
        public string SchoolAddress { get; set; }

        [Display(Name = "SchoolContact")]
        [Column(Name = "SchoolContact", DbType = "NVARCHAR")]
        public string SchoolContact { get; set; }

        [Display(Name = "SchoolEmail")]
        [Column(Name = "SchoolEmail", DbType = "NVARCHAR")]
        public string SchoolEmail { get; set; }

        [Display(Name = "IstallationDate")]
        [Column(Name = "IstallationDate", DbType = "NVARCHAR")]
        public string IstallationDate { get; set; }

       
        [Display(Name = "LicenseKey")]
        [Column(Name = "LicenseKey", DbType = "NVARCHAR")]
        public string LicenseKey { get; set; }

        [Display(Name = "LicenseStarts")]
        [Column(Name = "LicenseStarts", DbType = "NVARCHAR")]
        public string LicenseStarts { get; set; }

        [Display(Name = "LicenseEnds")]
        [Column(Name = "LicenseEnds", DbType = "NVARCHAR")]
        public string LicenseEnds { get; set; }

        [Display(Name = "InProcess")]
        [Column(Name = "InProcess", DbType = "BOOL")]
        public bool InProcess { get; set; }

        [Display(Name = "IsPayment")]
        [Column(Name = "IsPayment", DbType = "BOOL")]
        public bool IsPayment { get; set; }

    }
}
