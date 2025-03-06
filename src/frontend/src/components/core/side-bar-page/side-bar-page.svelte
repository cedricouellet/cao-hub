<script lang="ts">
  import type { Snippet } from "svelte";
  import { page } from "$app/state";

  import Button from "@components/core/button/button.svelte";

  export interface SideBarNavGroup {
    title: string;
    items: SideBarNavItem[];
  }

  export interface SideBarNavItem {
    startIcon?: string;
    text: string;
    href?: string;
    onClick?: CallableFunction;
  }

  interface SideBarProps {
    children: Snippet;
    navGroups: SideBarNavGroup[];
  }

  let props: SideBarProps = $props();

  let isOpen: boolean = $state(true);

  const toggleIsOpen = () => (isOpen = !isOpen);
</script>

<div class="flex flex-col md:flex-row md:h-full">
  <div
    class=" w-dvw md:w-[360px] bottom-0 shadow-lg min-h-full bg-neutral-dark-secondary border-t border-primary"
  >
    <div class="flex flex-row justify-end">
      <Button
        iconButton
        color="none"
        startIcon="bx {isOpen ? 'bxs-toggle-right' : 'bx-toggle-left'}"
        className="mt-18 text-light md:hidden text-xl"
        command={toggleIsOpen}
      />
    </div>
    <div
      class="md:ml-5 md:mt-5 transition-all delay-150 duration-400 {isOpen
        ? 'h-screen opacity-100 pb-3'
        : 'h-0 md:h-screen opacity-0 md:opacity-100'} ease-in-out"
    >
      <div class="md:fixed flex flex-col gap-4 ">
        {#each props.navGroups as group}
          <div>
            <div
              class="relative text-primary-light text-center md:text-start uppercase tracking-widest mb-2"
            >
              {group.title}
            </div>
            <div class="flex flex-col gap-2 md:pl-0">
              {#each group.items as item}
                <div
                  class="{page.route.id === item.href
                    ? 'text-light'
                    : 'text-light-disabled'} flex flex-row gap-1 justify-center md:justify-start"
                >
                  {#if item.startIcon}
                    <i class={item.startIcon}></i>
                  {/if}
                  <a href={item.href}>{item.text}</a>
                </div>
              {/each}
            </div>
          </div>
        {/each}
      </div>
    </div>
  </div>
  <div class="p-3 w-full">
    {@render props.children?.()}
  </div>
</div>
