﻿using Application.Common.Interfaces.WebUI;
using Application.ServiceBussiness.Dictionary;
using Domain.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private IIDentityService IDentityService { get; }
        public AuthorizationBehaviour(IIDentityService iIDentityService)
        {
            IDentityService = iIDentityService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (AuthorizeCommand.RolesPolicy.ContainsKey(request.GetType()))
            {
                var authorized = false;

                var roles = AuthorizeCommand.RolesPolicy[request.GetType()].Select(role => role + "");

                foreach (var roleSource in await IDentityService.GetRoles())
                {
                    if (roles.Contains(roleSource))
                    {
                        authorized = true;
                        break;
                    }

                }

                // Must be a member of at least one role in roles
                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }

            }
            //var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            //if (authorizeAttributes.Any())
            //{
            //    // Must be authenticated user
            //    if (_currentUserService.UserId == null)
            //    {
            //        throw new UnauthorizedAccessException();
            //    }

            //    // Role-based authorization
            //    var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

            //    if (authorizeAttributesWithRoles.Any())
            //    {
            //        var authorized = false;

            //        foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
            //        {
            //            foreach (var role in roles)
            //            {
            //                var isInRole = await _identityService.IsInRoleAsync(_currentUserService.UserId, role.Trim());
            //                if (isInRole)
            //                {
            //                    authorized = true;
            //                    break;
            //                }
            //            }
            //        }

            //        // Must be a member of at least one role in roles
            //        if (!authorized)
            //        {
            //            throw new ForbiddenAccessException();
            //        }
            //    }

            //    // Policy-based authorization
            //    var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
            //    if (authorizeAttributesWithPolicies.Any())
            //    {
            //        foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
            //        {
            //            var authorized = await _identityService.AuthorizeAsync(_currentUserService.UserId, policy);

            //            if (!authorized)
            //            {
            //                throw new ForbiddenAccessException();
            //            }
            //        }
            //    }
            //}

            // User is authorized / authorization not required
            return await next();
        }
    }
}
