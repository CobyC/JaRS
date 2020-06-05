using JARS.Entities;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JARS.SS.DTOs.Utils
{
    public static class StandardAppointmentConversionExtensions
    {
        public static StandardAppointmentDto ToStandardAppointmentDto(this StandardAppointment from)
        {
            StandardAppointmentDto to = from.ConvertTo<StandardAppointmentDto>();
            //to.StandardAppointmentExceptions = from.StandardAppointmentExceptions.ToList().ConvertAll(x => x.ConvertTo<StandardAppointmentExceptionDto>());
            return to;
        }

        public static List<StandardAppointmentDto> ToStandardAppointmentDtoList(this IList<StandardAppointment> fromList)
        {
            List<StandardAppointmentDto> toList = new List<StandardAppointmentDto>();
            foreach (StandardAppointment appointment in fromList)
            {
                toList.Add(appointment.ToStandardAppointmentDto());
            }
            return toList;
        }

        public static StandardAppointment FromStandardAppointmentDto(this StandardAppointmentDto from)
        {
            var to = from.ConvertTo<StandardAppointment>();
            //to.StandardAppointmentExceptions = from.StandardAppointmentExceptions.ConvertAll(x => x.ConvertTo<StandardAppointmentException>());
            return to;
        }

        public static List<StandardAppointment> FromStandardAppointmentDtoList(this IList<StandardAppointmentDto> fromList)
        {
            List<StandardAppointment> toList = new List<StandardAppointment>();
            foreach (StandardAppointmentDto appointment in fromList)
            {
                toList.Add(appointment.FromStandardAppointmentDto());
            }
            return toList;
        }
    }
}
