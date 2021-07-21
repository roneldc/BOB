//-----------------------------------------------------------------------
// <copyright file="EmailNotifyViewModel.cs" company="None">
//     Copyright (c) Allow to distribute this code and utilize this code for personal or commercial purpose.
// </copyright>
// <author>Asma Khalid</author>
//-----------------------------------------------------------------------

namespace EmailNotification.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Email notification view model class.
    /// </summary>
    public class EmailNotifyViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets to email address.
        /// </summary>
        [Required]
        [Display(Name = "To (Email Address)")]
        public string ToEmail { get; set; }

        #endregion
    }
}
