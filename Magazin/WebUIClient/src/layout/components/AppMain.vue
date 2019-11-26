<template>
    <div class="main-content">
        <div class="row">
            <div class="col-md-2" />
            <div class="col-md-8">
                <router-view />
            </div>
            <div class="col-md-2" />
        </div>
    </div>
</template>

<style lang="scss" scoped>
.main-content {
    padding-top: 2%;
    height: 750px;
}
</style>
