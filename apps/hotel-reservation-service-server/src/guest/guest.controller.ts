import * as common from "@nestjs/common";
import * as swagger from "@nestjs/swagger";
import { GuestService } from "./guest.service";
import { GuestControllerBase } from "./base/guest.controller.base";

@swagger.ApiTags("guests")
@common.Controller("guests")
export class GuestController extends GuestControllerBase {
  constructor(protected readonly service: GuestService) {
    super(service);
  }
}
