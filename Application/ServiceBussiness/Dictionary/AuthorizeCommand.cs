using Application.BaseCommand;
using Application.MediatorHandler.ClassRoom.Commands;
using Domain.Enumerations;
using System;
using System.Collections.Generic;

namespace Application.ServiceBussiness.Dictionary
{
    public class AuthorizeCommand
    {
        private static readonly Dictionary<Type, IEnumerable<RoleCode>> _dictionary =
            new Dictionary<Type, IEnumerable<RoleCode>>()
            {
                { typeof(CreateBaseCommand<Domain.Entities.AccountRole>), new RoleCode[]{ RoleCode.Administrator } },
                { typeof(CreateBaseCommand<Domain.Entities.Organization>), new RoleCode[]{ RoleCode.Administrator } },
                { typeof(CreateBaseCommand<Domain.Entities.SchoolYear>), new RoleCode[]{ RoleCode.Administrator, RoleCode.HeadMaster } },
                { typeof(CreateBaseCommand<Domain.Entities.Grade>), new RoleCode[]{ RoleCode.HeadMaster } },
                { typeof(CreateBaseCommand<Domain.Entities.ClassRoom>), new RoleCode[]{ RoleCode.HeadMaster } },
                { typeof(UpdateHomeRoomTeacherClassRoomCommand), new RoleCode[]{ RoleCode.HeadMaster } },
                { typeof(RenameClassRoomCommand), new RoleCode[]{ RoleCode.HeadMaster, RoleCode.Teacher } },
            };

        public static Dictionary<Type, IEnumerable<RoleCode>> RolesPolicy => _dictionary;
    }
}
