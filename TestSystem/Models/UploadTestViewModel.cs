﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestSystem.Models
{
    public class UploadTestViewModel
    {

        [Required]
        [Display(Name = "Test file name")]
        [StringLength(30, MinimumLength = 3)]
        public string TestName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        [StringLength(300)]
        public string Description { get; set; }

        [Required (ErrorMessage = "Please, attach test file.")]
        public HttpPostedFileBase file { get; set; }

    }
}