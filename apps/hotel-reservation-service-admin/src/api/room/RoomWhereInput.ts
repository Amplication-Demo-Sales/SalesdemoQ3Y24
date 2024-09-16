import { BookingListRelationFilter } from "../booking/BookingListRelationFilter";
import { IntNullableFilter } from "../../util/IntNullableFilter";
import { StringNullableFilter } from "../../util/StringNullableFilter";
import { StringFilter } from "../../util/StringFilter";

export type RoomWhereInput = {
  bookings?: BookingListRelationFilter;
  capacity?: IntNullableFilter;
  features?: StringNullableFilter;
  id?: StringFilter;
  roomType?: StringNullableFilter;
};
