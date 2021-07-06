using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CaseStudyFlippler.Application.Dtos
{
    public class ReminderSearchRequestDto
    {
        public string SearchText { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        
        public bool HasQuery
        {
            get
            {
                return (
                    !String.IsNullOrWhiteSpace(SearchText)
                    || StartDate != default
                    || EndDate != default
                    || Page > 0
                    );
            }
        }
    }
}
