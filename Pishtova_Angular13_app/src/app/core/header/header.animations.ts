import { trigger, transition, useAnimation, animation, style, animate } from '@angular/animations';

export const EnterAnimation = animation([
    style({ opacity: 0.5 }),
    animate(
      '{{ duration }} cubic-bezier(0.59, 0.32, 0.38, 1.13)',
      style({  opacity: 1 })
    ),
  ]);

export const ExitAnimation = animation([
  style({ opacity: 1 }),
  animate(
    '{{ duration }} cubic-bezier(0.59, 0.32, 0.38, 1.13)',
    style({  opacity: 0 })
  ),
]);


export const ModalAnimation = trigger('slideRight', [
    transition(
      ':enter',
      useAnimation(EnterAnimation, {
        params: { x: `${window.innerWidth}px`, y: 0, duration: '0.5s' },
      })
    ),
    transition(
      ':leave',
      useAnimation(ExitAnimation, {
        params: {duration: '0.5s' },
      })
    ),
  ]);
