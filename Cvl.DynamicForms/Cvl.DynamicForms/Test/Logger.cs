using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.DynamicForms.Test
{
    public class Logger
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public ICollection<Logger> Subloggers { get; set; } = new List<Logger>();

        /// <summary>
        /// 
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Now;

        /// <summary>
        /// Nazwa metody
        /// </summary>
        public string Member { get; set; }

        /// <summary>
        /// Typ logu
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Wiadomość logu
        /// </summary>
        public string Message { get; set; }

        public string SourceCodePath { get; set; }
        public int SourceCodeLine { get; set; }
    }
}
