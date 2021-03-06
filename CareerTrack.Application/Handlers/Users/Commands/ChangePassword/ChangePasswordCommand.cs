﻿using MediatR;

namespace CareerTrack.Application.Handlers.Users.Commands.ChangePassword
{
    public class ChangePasswordCommand : UserCommandRequestBase, IRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
