using System;

namespace CaseStudyFlippler.Application
{
    public class BusinessException : Exception
    {
        public BusinessException()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }

    }
}
