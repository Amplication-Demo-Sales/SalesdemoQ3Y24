import { DateTimeNullableFilter } from "../../util/DateTimeNullableFilter";
import { GuestWhereUniqueInput } from "../guest/GuestWhereUniqueInput";
import { StringFilter } from "../../util/StringFilter";
import { RoomWhereUniqueInput } from "../room/RoomWhereUniqueInput";
import { FloatNullableFilter } from "../../util/FloatNullableFilter";

export type BookingWhereInput = {
  checkInDate?: DateTimeNullableFilter;
  checkOutDate?: DateTimeNullableFilter;
  guest?: GuestWhereUniqueInput;
  id?: StringFilter;
  room?: RoomWhereUniqueInput;
  totalPrice?: FloatNullableFilter;
};
