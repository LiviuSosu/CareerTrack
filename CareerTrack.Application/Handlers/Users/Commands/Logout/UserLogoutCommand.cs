﻿using MediatR;

namespace CareerTrack.Application.Handlers.Users.Commands.Logout
{
    public class UserLogoutCommand : UserCommandRequestBase, IRequest
    {
        public string Token { get; set; }
    }
}
