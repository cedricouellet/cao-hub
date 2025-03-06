import type { Snippet } from "svelte";

interface SideBarNavGroup {
    title: string;
    items: SideBarNavItem[];
}

interface SideBarNavItem {
    startIcon?: string;
    text: string;
    href?: string;
    onClick?: CallableFunction;
}

interface SideBarProps {
    children: Snippet;
    navGroups: SideBarNavGroup[];
}