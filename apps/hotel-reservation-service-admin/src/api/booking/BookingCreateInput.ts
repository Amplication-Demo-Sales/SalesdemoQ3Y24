import { GuestWhereUniqueInput } from "../guest/GuestWhereUniqueInput";
import { RoomWhereUniqueInput } from "../room/RoomWhereUniqueInput";

export type BookingCreateInput = {
  checkInDate?: Date | null;
  checkOutDate?: Date | null;
  guest?: GuestWhereUniqueInput | null;
  room?: RoomWhereUniqueInput | null;
  totalPrice?: number | null;
};
