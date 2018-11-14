import { SafeUrl } from "@angular/platform-browser";

export class Image {
  id: string;
  base64: string;
  name: string;
  safeBase64: SafeUrl;
}
