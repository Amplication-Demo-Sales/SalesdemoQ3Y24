import { Booking } from "../booking/Booking";

export type Room = {
  bookings?: Array<Booking>;
  capacity: number | null;
  createdAt: Date;
  features: string | null;
  id: string;
  roomType: string | null;
  updatedAt: Date;
};
