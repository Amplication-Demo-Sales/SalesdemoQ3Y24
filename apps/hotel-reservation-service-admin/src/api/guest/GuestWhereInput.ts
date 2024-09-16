import { BookingListRelationFilter } from "../booking/BookingListRelationFilter";
import { StringNullableFilter } from "../../util/StringNullableFilter";
import { StringFilter } from "../../util/StringFilter";

export type GuestWhereInput = {
  bookings?: BookingListRelationFilter;
  contactInfo?: StringNullableFilter;
  id?: StringFilter;
  name?: StringNullableFilter;
  paymentInfo?: StringNullableFilter;
};
