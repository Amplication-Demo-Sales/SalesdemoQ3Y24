import * as React from "react";

import {
  Show,
  SimpleShowLayout,
  ShowProps,
  TextField,
  DateField,
  ReferenceManyField,
  Datagrid,
  ReferenceField,
} from "react-admin";

import { GUEST_TITLE_FIELD } from "./GuestTitle";
import { ROOM_TITLE_FIELD } from "../room/RoomTitle";

export const GuestShow = (props: ShowProps): React.ReactElement => {
  return (
    <Show {...props}>
      <SimpleShowLayout>
        <TextField label="ContactInfo" source="contactInfo" />
        <DateField source="createdAt" label="Created At" />
        <TextField label="ID" source="id" />
        <TextField label="Name" source="name" />
        <TextField label="PaymentInfo" source="paymentInfo" />
        <DateField source="updatedAt" label="Updated At" />
        <ReferenceManyField
          reference="Booking"
          target="guestId"
          label="Bookings"
        >
          <Datagrid rowClick="show" bulkActionButtons={false}>
            <TextField label="CheckInDate" source="checkInDate" />
            <TextField label="CheckOutDate" source="checkOutDate" />
            <DateField source="createdAt" label="Created At" />
            <ReferenceField label="Guest" source="guest.id" reference="Guest">
              <TextField source={GUEST_TITLE_FIELD} />
            </ReferenceField>
            <TextField label="ID" source="id" />
            <ReferenceField label="Room" source="room.id" reference="Room">
              <TextField source={ROOM_TITLE_FIELD} />
            </ReferenceField>
            <TextField label="TotalPrice" source="totalPrice" />
            <DateField source="updatedAt" label="Updated At" />
          </Datagrid>
        </ReferenceManyField>
      </SimpleShowLayout>
    </Show>
  );
};
