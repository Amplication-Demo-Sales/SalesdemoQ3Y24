import { BookingCreateNestedManyWithoutGuestsInput } from "./BookingCreateNestedManyWithoutGuestsInput";

export type GuestCreateInput = {
  bookings?: BookingCreateNestedManyWithoutGuestsInput;
  contactInfo?: string | null;
  name?: string | null;
  paymentInfo?: string | null;
};
