﻿using JARS.Core.Data.Interfaces.Repositories;
using JARS.Data.NH.Interfaces;
using JARS.Entities;

namespace JARS.Data.NH.Jars.Interfaces
{
    public interface IStandardAppointmentRepository : IStandardAppointmentRepositoryBase, IDataRepositoryEagerly<StandardAppointment>
    {

    }
}
