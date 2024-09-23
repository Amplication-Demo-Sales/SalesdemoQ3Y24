import * as React from "react";
import { List, Datagrid, ListProps, TextField, DateField } from "react-admin";
import Pagination from "../Components/Pagination";

export const RoomList = (props: ListProps): React.ReactElement => {
  return (
    <List {...props} title={"Rooms"} perPage={50} pagination={<Pagination />}>
      <Datagrid rowClick="show" bulkActionButtons={false}>
        <TextField label="Capacity" source="capacity" />
        <DateField source="createdAt" label="Created At" />
        <TextField label="Features" source="features" />
        <TextField label="ID" source="id" />
        <TextField label="RoomType" source="roomType" />
        <DateField source="updatedAt" label="Updated At" />{" "}
      </Datagrid>
    </List>
  );
};
