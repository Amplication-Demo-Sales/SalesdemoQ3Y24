import * as React from "react";
import { List, Datagrid, ListProps, TextField, DateField } from "react-admin";
import Pagination from "../Components/Pagination";

export const GuestList = (props: ListProps): React.ReactElement => {
  return (
    <List {...props} title={"Guests"} perPage={50} pagination={<Pagination />}>
      <Datagrid rowClick="show" bulkActionButtons={false}>
        <TextField label="ContactInfo" source="contactInfo" />
        <DateField source="createdAt" label="Created At" />
        <TextField label="ID" source="id" />
        <TextField label="Name" source="name" />
        <TextField label="PaymentInfo" source="paymentInfo" />
        <DateField source="updatedAt" label="Updated At" />{" "}
      </Datagrid>
    </List>
  );
};
