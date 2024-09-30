import { UserWhereUniqueInput } from "../user/UserWhereUniqueInput";

export type OrderUpdateInput = {
  orderDate?: Date | null;
  quantity?: number | null;
  user?: UserWhereUniqueInput | null;
};
