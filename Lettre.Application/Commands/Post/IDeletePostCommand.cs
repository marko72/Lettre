﻿using Lettre.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.Commands.Post
{
    public interface IDeletePostCommand: ICommand<int>
    {
    }
}
