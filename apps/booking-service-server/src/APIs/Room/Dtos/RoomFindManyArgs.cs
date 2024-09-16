using BookingService.APIs.Common;
using BookingService.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class RoomFindManyArgs : FindManyInput<Room, RoomWhereInput> { }
