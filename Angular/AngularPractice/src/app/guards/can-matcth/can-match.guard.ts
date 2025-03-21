import { CanMatchFn } from '@angular/router';

export const canMatchGuard: CanMatchFn = (route, segments) => {
  console.log(segments[0]);
  console.log(segments);

  return true;
};
