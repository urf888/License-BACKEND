using System;
using System.Collections.Generic;

namespace models;

public partial class User
{

    public int? IdUser { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }

    public string? Surname { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }


}