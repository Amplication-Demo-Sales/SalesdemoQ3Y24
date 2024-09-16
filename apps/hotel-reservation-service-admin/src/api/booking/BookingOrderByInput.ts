import { SortOrder } from "../../util/SortOrder";

export type BookingOrderByInput = {
  checkInDate?: SortOrder;
  checkOutDate?: SortOrder;
  createdAt?: SortOrder;
  guestId?: SortOrder;
  id?: SortOrder;
  roomId?: SortOrder;
  totalPrice?: SortOrder;
  updatedAt?: SortOrder;
};
