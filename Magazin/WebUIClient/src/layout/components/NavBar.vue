<template>    
    <b-navbar class="main-menu" toggleable="lg" type="dark">
        <b-navbar-toggle target="nav-collapse">          
        </b-navbar-toggle>

        <b-collapse id="nav-collapse" is-nav>
            <b-navbar-nav align="left" class="main-nav">
                <b-nav-item href="/">Главная</b-nav-item>
                <b-nav-item href="/products">Продукция</b-nav-item>
                <b-nav-item href="/news">Новости</b-nav-item>
                <b-nav-item href="/about">О нас</b-nav-item>
            </b-navbar-nav>
        </b-collapse>
    </b-navbar>
</template>

<script>
export default {
    computed: {
        styling() {

        }
    }
}
</script>

<style lang="scss" scoped>
  @import "~@/styles/variables.scss";

.main-menu {
    background-color: $menuBg;
    color: #fff;    
}
    :hover.main-menu > li {
        background-color: #344433;
    }

.main-nav {
    width: 100%;
    padding-left: 20%;
}
</style>