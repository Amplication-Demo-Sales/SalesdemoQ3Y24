import { BookingUpdateManyWithoutGuestsInput } from "./BookingUpdateManyWithoutGuestsInput";

export type GuestUpdateInput = {
  bookings?: BookingUpdateManyWithoutGuestsInput;
  contactInfo?: string | null;
  name?: string | null;
  paymentInfo?: string | null;
};
