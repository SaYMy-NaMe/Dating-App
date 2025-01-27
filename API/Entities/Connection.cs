using System;
using AutoMapper.Configuration.Conventions;

namespace API.Entities;

public class Connection
{
    public required string ConnectionId { get; set; }
    public required string Username { get; set; }
}
