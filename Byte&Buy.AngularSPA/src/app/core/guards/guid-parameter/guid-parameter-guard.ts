import { CanMatchFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { Guid } from 'guid-typescript';

export const guidParameterGuard: CanMatchFn = (route, segments) => {
  const router = inject(Router);

  const path = route.path;
  if (!path) {
    return true;
  }

  const pathParts = path.split('/');
  const idIndex = pathParts.indexOf(':id');

  if (idIndex === -1) {
    return true;
  }

  const id = segments[idIndex]?.path;
  if (!id) {
    return router.parseUrl('/not-found');
  }

  if(!Guid.isGuid(id))
    return router.parseUrl('/not-found');

  return true;
};