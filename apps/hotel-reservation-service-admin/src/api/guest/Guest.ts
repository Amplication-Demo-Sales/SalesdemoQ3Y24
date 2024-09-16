import { Booking } from "../booking/Booking";

export type Guest = {
  bookings?: Array<Booking>;
  contactInfo: string | null;
  createdAt: Date;
  id: string;
  name: string | null;
  paymentInfo: string | null;
  updatedAt: Date;
};
