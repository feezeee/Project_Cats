﻿namespace Cat.API.Request
{
    public class PostUserRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
