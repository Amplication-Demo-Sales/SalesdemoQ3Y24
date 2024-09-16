import { SortOrder } from "../../util/SortOrder";

export type RoomOrderByInput = {
  capacity?: SortOrder;
  createdAt?: SortOrder;
  features?: SortOrder;
  id?: SortOrder;
  roomType?: SortOrder;
  updatedAt?: SortOrder;
};
