import { SortOrder } from "../../util/SortOrder";

export type GuestOrderByInput = {
  contactInfo?: SortOrder;
  createdAt?: SortOrder;
  id?: SortOrder;
  name?: SortOrder;
  paymentInfo?: SortOrder;
  updatedAt?: SortOrder;
};
