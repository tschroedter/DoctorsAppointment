﻿using Nancy;

namespace Demo.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get [ "/" ] = _ => View [ "index" ];
        }
    }
}