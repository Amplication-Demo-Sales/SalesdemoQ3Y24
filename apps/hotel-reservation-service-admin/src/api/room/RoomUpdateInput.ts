import { BookingUpdateManyWithoutRoomsInput } from "./BookingUpdateManyWithoutRoomsInput";

export type RoomUpdateInput = {
  bookings?: BookingUpdateManyWithoutRoomsInput;
  capacity?: number | null;
  features?: string | null;
  roomType?: string | null;
};
