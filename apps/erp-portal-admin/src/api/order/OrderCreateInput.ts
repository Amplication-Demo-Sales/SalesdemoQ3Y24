import { UserWhereUniqueInput } from "../user/UserWhereUniqueInput";

export type OrderCreateInput = {
  orderDate?: Date | null;
  quantity?: number | null;
  user?: UserWhereUniqueInput | null;
};
