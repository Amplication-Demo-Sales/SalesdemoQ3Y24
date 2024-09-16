import { Guest } from "../guest/Guest";
import { Room } from "../room/Room";

export type Booking = {
  checkInDate: Date | null;
  checkOutDate: Date | null;
  createdAt: Date;
  guest?: Guest | null;
  id: string;
  room?: Room | null;
  totalPrice: number | null;
  updatedAt: Date;
};
